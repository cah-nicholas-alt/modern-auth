import { BrowserRouter as Router, Redirect, Route, Switch } from "react-router-dom";
import Navigation from "./Navigation";
import AccountHome from "./AccountHome";
import Login from "./Login";
import { LoginContext } from "./LoginContext";
import { useState } from "react";

function App() {
  const [isLoggedIn, setLoggedIn] = useState(false);

  return (
  <LoginContext.Provider value={{isLoggedIn, setLoggedIn}}>
      <Router>
        <Navigation />
        <Switch>
          <Route path="/Login" component={Login} />
          <Route>
            {isLoggedIn
              ? (<AccountHome />)
              : (<Redirect to="/Login" />)
            }
          </Route>
        </Switch>
      </Router>
    </LoginContext.Provider>
  );
}

export default App;
