import { Card, Container } from "react-bootstrap";
import { useLoginContext } from "./LoginContext";

function Token() {
    const { loginState } = useLoginContext();

    return (
        <Container className="mt-3">
            <Card className="p-3">
                {loginState.token}
            </Card>
        </Container>
    );
}

export default Token;