import { useCallback, useState } from 'react';
import { useEffect } from 'react';
import { Button, Card, Container } from 'react-bootstrap';
import { useLoginContext } from './LoginContext';
import { TransferModal } from './TransferModal';

function AccountHome() {
  const { loginState } = useLoginContext();
  const [account, setAccount] = useState([]);
  const [showModal, setShowModal] = useState(false);

  const loadAccount = useCallback(async () => {
    const resp = await fetch('http://accounts.api.pursuit.local:5001/api/accounts', {
      headers: {
        Authorization: `Bearer ${loginState.token}`,
      },
    });
    const json = await resp.json();
    setAccount(json);
  }, [loginState]);

  useEffect(() => {
    loadAccount();
  }, [loginState, loadAccount]);

  return (
    <Container className="mt-3">
      {account.map((a) => (
        <Card className="text-dark p-3 mt-3">
          <div>{a.accountName}</div>
          <div>Balance: {a && a.balance}</div>
        </Card>
      ))}

      <Button className="mt-3" onClick={() => setShowModal(true)}>
        Transfer Funds
      </Button>

      <TransferModal show={showModal} onHide={() => setShowModal(false)} onRequestReload={loadAccount} />
    </Container>
  );
}

export default AccountHome;
