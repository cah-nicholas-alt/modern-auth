import { useState } from 'react';
import { useEffect } from 'react';
import { Card, Container } from 'react-bootstrap';
import { useLoginContext } from './LoginContext';

function AccountHome() {
  const { loginState } = useLoginContext();
  const [account, setAccount] = useState(null);

  useEffect(() => {
    const fn = async () => {
      const resp = await fetch('http://accounts.api.pursuit.local:5001/account', {
        headers: {
          Authorization: `Bearer ${loginState.token}`,
        },
      });
      const json = await resp.json();
      // await new Promise((resolve) => setTimeout(resolve, 500));
      setAccount(json);
    };

    fn();
  }, [loginState]);

  return (
    <Container className="mt-3">
      <Card className="text-dark p-3">
        <div>Account Home</div>
        <div>Balance: {account && account[0].balance}</div>
      </Card>
    </Container>
  );
}

export default AccountHome;
