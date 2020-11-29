import { Card, Container } from 'react-bootstrap';
import { useLoginContext } from './LoginContext';

function Token() {
  const { loginState } = useLoginContext();

  const tokenSections = loginState.token.split('.');
  const decodedToken = JSON.stringify(loginState.decodedToken, null, 2);

  return (
    <Container className="mt-3">
      <Card className="p-3">
        <span className="text-primary">{tokenSections[0]}</span>
        <b>.</b>
        <span className="text-success">{tokenSections[1]}</span>
        <b>.</b>
        <span className="text-info">{tokenSections[2]}</span>
      </Card>
      <Card className="p-3 mt-3">
        <pre>{decodedToken}</pre>
      </Card>
    </Container>
  );
}

export default Token;
