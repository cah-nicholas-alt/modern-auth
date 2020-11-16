import { useState } from "react";
import { Button, Form } from "react-bootstrap";
import { useHistory } from "react-router-dom";
import styles from "./Login.module.css";
import { useLoginContext } from "./LoginContext";

function Login() {
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [submitting, setSubmitting] = useState(false);

    const { setLoggedIn } = useLoginContext();
    const history = useHistory();

    function validateForm() {
        return username.length > 0 && password.length > 0;
    }

    async function handleSubmit(event) {
        event.preventDefault();


        function sleep(ms) {
            return new Promise(resolve => setTimeout(resolve, ms));
        }

        setSubmitting(true);
        await sleep(500);
        if (username === 'alice' && password === "a123")
        {
            setLoggedIn(true);
            history.push("/");
        }


        setSubmitting(false);
    }

    return (
        <div className={styles.login}>
            <Form onSubmit={handleSubmit}>
                <Form.Group size="lg" controlId="username">
                    <Form.Label>Username</Form.Label>
                    <Form.Control autoFocus type="username" value={username} onChange={(e) => setUsername(e.target.value)} />
                </Form.Group>
                <Form.Group size="lg" controlId="password">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
                </Form.Group>
                <Button block size="lg" type="submit" disabled={!validateForm() || submitting}>
                    Login
                </Button>
            </Form>
        </div>
    )
}

export default Login;