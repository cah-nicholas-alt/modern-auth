import React, { useEffect, useState } from 'react';
import { Button, Form, Modal } from 'react-bootstrap';
import { useLoginContext } from './LoginContext';

export function TransferModal({ onRequestReload, onHide, ...rest }) {
  const { loginState } = useLoginContext();
  const [accountList, setAccountList] = useState([]);
  const [sourceAccount, setSourceAccount] = useState('');
  const [targetAccount, setTargetAccount] = useState('');
  const [amount, setAmount] = useState(0);

  const clearForm = () => {
    setSourceAccount('');
    setTargetAccount('');
    setAmount(0);
  };

  useEffect(() => {
    const fn = async () => {
      var resp = await fetch('http://accounts.api.pursuit.local:5001/accounts/all');
      var json = await resp.json();
      setAccountList(json);
    };

    fn();
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    console.log(e);

    await fetch('http://accounts.api.pursuit.local:5001/accounts/', {
      method: 'POST',
      body: JSON.stringify({
        sourceAccount,
        targetAccount,
        amount,
      }),
      headers: {
        Authorization: `Bearer ${loginState.token}`,
        'Content-Type': 'application/json',
      },
    });

    await onRequestReload();
    onHideWrapped();
  };

  const onHideWrapped = () => {
    clearForm();
    onHide();
  };

  return (
    <Modal {...rest} onHide={onHideWrapped}>
      <Modal.Body>
        <Form onSubmit={handleSubmit}>
          <Form.Group>
            <Form.Label>From:</Form.Label>
            <Form.Control as="select" value={sourceAccount} onChange={(e) => setSourceAccount(e.target.value)}>
              <option disabled value=""></option>
              {accountList
                .filter((a) => a.userId.replace(/-/g, '').toUpperCase() === loginState.userId)
                .map((a) => (
                  <option key={a.accountName} value={a.accountId}>
                    {a.accountName}
                  </option>
                ))}
            </Form.Control>
          </Form.Group>
          <Form.Group>
            <Form.Label>To</Form.Label>
            <Form.Control as="select" value={targetAccount} onChange={(e) => setTargetAccount(e.target.value)}>
              <option disabled value=""></option>
              {accountList
                .filter((a) => a.userId.replace(/-/g, '').toUpperCase() !== loginState.userId)
                .map((a) => (
                  <option key={a.accountName} value={a.accountId}>
                    {a.accountName}
                  </option>
                ))}
            </Form.Control>
          </Form.Group>
          <Form.Group>
            <Form.Label>Amount:</Form.Label>
            <Form.Control type="number" value={amount} onChange={(e) => setAmount(e.target.value)}></Form.Control>
          </Form.Group>
          <Form.Group>
            <Button variant="primary" type="submit">
              Send
            </Button>
          </Form.Group>
        </Form>
      </Modal.Body>
    </Modal>
  );
}
