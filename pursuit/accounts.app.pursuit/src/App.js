import { BrowserRouter as Router, Redirect, Route, Switch } from 'react-router-dom';
import Navigation from './Navigation';
import AccountHome from './AccountHome';
import Login from './Login';
import { LoginContext } from './LoginContext';
import { useEffect, useState } from 'react';
import Token from './Token';
import LoginCallback from './LoginCallback';

function App() {
  let initLoginState = localStorage.getItem('session');
  if (initLoginState) {
    initLoginState = JSON.parse(initLoginState) || null;
  }

  const [loginState, setLoginState] = useState(initLoginState);

  useEffect(() => {
    localStorage.setItem('session', JSON.stringify(loginState));
    if (loginState) {
      const timeout = setTimeout(() => setLoginState(null), loginState.exp - new Date().getTime());
      return () => clearTimeout(timeout);
    }
  }, [loginState]);

  return (
    <LoginContext.Provider value={{ loginState, setLoginState }}>
      <Router>
        <Navigation />
        <Switch>
          <Route path="/LoginCallback" component={LoginCallback} />
          <Route path="/Login" component={Login} />
          {loginState ? (
            <>
              <Switch>
                <Route path="/token">
                  <Token />
                </Route>
                <Route>
                  <AccountHome />
                </Route>
              </Switch>
            </>
          ) : (
            <Redirect to="/Login" />
          )}
        </Switch>
      </Router>
    </LoginContext.Provider>
  );
}

export default App;
