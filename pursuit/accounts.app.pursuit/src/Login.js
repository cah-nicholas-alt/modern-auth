import jwtDecode from 'jwt-decode';
import { useEffect, useState } from 'react';
import { Alert, Button, Card, Container, Form } from 'react-bootstrap';
import { generatePath, Redirect, useHistory, useRouteMatch } from 'react-router-dom';
import styles from './Login.module.css';
import { useLoginContext } from './LoginContext';
import Oidc from 'oidc-client';

// function Login() {
//   const { loginState, setLoginState } = useLoginContext();

//   const [username, setUsername] = useState('');
//   const [password, setPassword] = useState('');
//   const [submitting, setSubmitting] = useState(false);
//   const [error, setError] = useState('');

//   const history = useHistory();

//   if (loginState) return <Redirect to="/account" />;

//   function validateForm() {
//     return username.length > 0 && password.length > 0;
//   }

//   function handleSubmitPasswordFlow(event) {
//     event.preventDefault();
//     setSubmitting(true);

//     const client_id = 'Pursuit.Accounts.App';

//     fetch('http://id.pursuit.local:5000/connect/token', {
//       method: 'POST',
//       body: `grant_type=password&client_id=${client_id}&username=${username}&password=${password}`,
//       headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
//     })
//       .then((resp) => {
//         if (!resp.ok) {
//           throw resp;
//         }
//         return resp.json();
//       })
//       .then((json) => {
//         setLoginState({
//           token: json.access_token,
//           exp: new Date().getTime() + json.expires_in * 1000,
//           decodedToken: jwtDecode(json.access_token),
//           userId: jwtDecode(json.access_token).sub,
//         });
//         history.push('/');
//       })
//       .catch((errResp) => {
//         errResp.json
//           ? errResp.json().then((json) => setError(json.error_description))
//           : setError('System error, please try again.');
//       })
//       .finally(() => {
//         setSubmitting(false);
//       });
//   }

//   return (
//     <Container className={`${styles.loginCard} mt-3`}>
//       <Card className="p-5">
//         <Form onSubmit={handleSubmitPasswordFlow} className="">
//           <Form.Group size="lg" controlId="username">
//             <Form.Label>Username</Form.Label>
//             <Form.Control autoFocus type="username" value={username} onChange={(e) => setUsername(e.target.value)} />
//           </Form.Group>
//           <Form.Group size="lg" controlId="password">
//             <Form.Label>Password</Form.Label>
//             <Form.Control type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
//           </Form.Group>
//           <Button block size="lg" type="submit" disabled={!validateForm() || submitting}>
//             Login
//           </Button>
//           {error && (
//             <Alert variant="danger" className="mt-3">
//               {error}
//             </Alert>
//           )}
//         </Form>
//       </Card>
//     </Container>
//   );
// }

function Login() {
  //Not Suitable for Production, race cases w/ React Re-Rendering need to be resolved...
  const { setLoginState } = useLoginContext();
  const history = useHistory();
  const match = useRouteMatch('/LoginCallback');
  if (match) {
    new Oidc.UserManager({ response_mode: 'fragment' }).signinRedirectCallback().then(function (user) {
      setLoginState({
        token: user.access_token,
        exp: user.expires_at,
        decodedToken: jwtDecode(user.access_token),
        userId: jwtDecode(user.access_token).sub,
      });
      history.push('/');
    });
  } else {
    var mgr = new Oidc.UserManager({
      authority: 'http://id.pursuit.local:5000',
      client_id: 'Pursuit.Accounts.App',
      redirect_uri: 'http://accounts.app.pursuit.local:5003/LoginCallback',
      response_type: 'token id_token',
      scope: 'openid profile PursuitAccountsApi.ReadAccounts PursuitAccountsApi.Admin',
      post_logout_redirect_uri: 'http://accounts.app.pursuit.local:5003',
    });
    console.log('called');

    mgr.signinRedirect();
  }

  return <div></div>;
}

export default Login;
