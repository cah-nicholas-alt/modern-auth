import styles from "./Navigation.module.css";
import { Navbar, Nav, Button } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { useLoginContext } from "./LoginContext";
import { useHistory } from "react-router-dom";

function Navigation() {
    const { isLoggedIn, setLoggedIn } = useLoginContext();
    const history = useHistory();

    function handleLogout() {
        setLoggedIn(false);
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
                    <LinkContainer to="account">
                        <Nav.Link>
                            Acount
                        </Nav.Link>
                    </LinkContainer>
                </Nav>
                <Nav activeKey={window.location.pathname} className="ml-auto">
                    { isLoggedIn
                        ? (<Button onClick={handleLogout}>Logout</Button>)
                        : (<LinkContainer to="login" >
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