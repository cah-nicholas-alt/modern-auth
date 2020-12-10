import jwtDecode from 'jwt-decode';
import Oidc from 'oidc-client';
import { useEffect } from 'react';
import { Redirect } from 'react-router-dom';
import { useLoginContext } from './LoginContext';

function LoginCallback() {
  const { loginState, setLoginState } = useLoginContext();

  useEffect(() => {
    new Oidc.UserManager({ response_mode: 'fragment' }).signinRedirectCallback().then(function (user) {
      setLoginState({
        token: user.access_token,
        exp: user.expires_at,
        decodedToken: jwtDecode(user.access_token),
        userId: jwtDecode(user.access_token).sub,
      });
    });
  }, []);

  if (loginState) return <Redirect to="/" />;

  return <div></div>;
}

export default LoginCallback;
