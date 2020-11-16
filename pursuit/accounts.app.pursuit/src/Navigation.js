import styles from "./Navigation.module.css";
import { Navbar, Nav, Button } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { useLoginContext } from "./LoginContext";
import { useHistory } from "react-router-dom";

function Navigation() {
    const { loginState, setLoginState } = useLoginContext();
    const history = useHistory();

    function handleLogout() {
        setLoginState(null);
        history.push("/login");
    }

    return (
        <Navbar collapseOnSelect bg="dark" expand="md" className="mb navbar-dark">
            <Navbar.Brand>
                <img src="pursuit-logo.png" alt="logo" draggable={false} className={`${styles.navBarShort} ${styles.logoLarge}`} /> Pursuit Bank
            </Navbar.Brand>
            <Navbar.Toggle />
            <Navbar.Collapse>
                <Nav activeKey={window.location.pathname}>
                    { loginState &&
                        <>
                            <LinkContainer to="account">
                                <Nav.Link>
                                    Acount
                                </Nav.Link>
                            </LinkContainer>
                            <LinkContainer to="token">
                                <Nav.Link>
                                    Token
                                </Nav.Link>
                            </LinkContainer>
                        </>
                    }
                </Nav>
                <Nav activeKey={window.location.pathname} className="ml-auto">
                    { loginState
                        ? ( <Button onClick={handleLogout}>Logout</Button>)
                        : ( <LinkContainer to="login" >
                                <Nav.Link >
                                    <Button>Login</Button>
                                </Nav.Link>
                            </LinkContainer>)
                    }
                </Nav>
            </Navbar.Collapse>
        </Navbar>
    );
}

export default Navigation;