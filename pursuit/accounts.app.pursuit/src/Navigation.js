import styles from './Navigation.module.css';
import { Navbar, Nav, Button } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import { useLoginContext } from './LoginContext';
import { useHistory } from 'react-router-dom';
import Oidc from 'oidc-client';

function Navigation() {
  const { loginState, setLoginState } = useLoginContext();
  const history = useHistory();

  function handleLogout() {
    // setLoginState(null);
    // history.push('/login');

    //UNCOMMENT TO ENABLE OIDC
    var mgr = new Oidc.UserManager({
      authority: 'http://id.pursuit.local:5000',
      client_id: 'Pursuit.Accounts.App',
      redirect_uri: 'http://accounts.app.pursuit.local:5003/LoginCallback',
      response_type: 'token id_token',
      scope: 'openid profile PursuitAccountsApi.ReadAccounts PursuitAccountsApi.Admin',
      post_logout_redirect_uri: 'http://accounts.app.pursuit.local:5003',
    });

    mgr.signoutRedirect();
    setLoginState(null);
  }

  return (
    <Navbar collapseOnSelect bg="dark" expand="md" className="mb navbar-dark">
      <Navbar.Brand>
        <img
          src="pursuit-logo.png"
          alt="logo"
          draggable={false}
          className={`${styles.navBarShort} ${styles.logoLarge}`}
        />{' '}
        Pursuit Bank
      </Navbar.Brand>
      <Navbar.Toggle />
      <Navbar.Collapse>
        <Nav activeKey={window.location.pathname}>
          {loginState && (
            <>
              <LinkContainer to="account">
                <Nav.Link>Acount</Nav.Link>
              </LinkContainer>
              <LinkContainer to="token">
                <Nav.Link>Token</Nav.Link>
              </LinkContainer>
            </>
          )}
        </Nav>
        <Nav activeKey={window.location.pathname} className="ml-auto">
          {loginState ? (
            <Button onClick={handleLogout}>Logout</Button>
          ) : (
            <LinkContainer to="login">
              <Nav.Link>
                <Button>Login</Button>
              </Nav.Link>
            </LinkContainer>
          )}
        </Nav>
      </Navbar.Collapse>
    </Navbar>
  );
}

export default Navigation;
