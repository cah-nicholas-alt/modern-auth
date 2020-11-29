import { Card, Container } from 'react-bootstrap';
import { useLoginContext } from './LoginContext';
import jwt_decode from 'jwt-decode';

function Token() {
  const { loginState } = useLoginContext();

  const decodedToken = JSON.stringify(loginState.decodedToken, null, 2);
  return (
    <Container className="mt-3">
      <Card className="p-3">{loginState.token}</Card>
      <Card className="p-3 mt-3">
        <pre>{decodedToken}</pre>
      </Card>
    </Container>
  );
}

export default Token;
