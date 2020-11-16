import { BrowserRouter as Router, Redirect, Route, Switch } from "react-router-dom";
import Navigation from "./Navigation";
import AccountHome from "./AccountHome";
import Login from "./Login";
import { LoginContext } from "./LoginContext";
import { useState } from "react";
import Token from "./Token";

function App() {
    const [loginState, setLoginState] = useState(null);

    return (
        <LoginContext.Provider value={{ loginState, setLoginState }}>
            <Router>
                <Navigation />
                <Switch>
                    <Route path="/Login" component={Login} />
                    { loginState
                        ? ( <>
                                <Switch>
                                    <Route path="/token">
                                        <Token />
                                    </Route>
                                    <Route>
                                        <AccountHome />
                                    </Route>
                                </Switch>
                            </>)
                        : ( <Redirect to="/Login" />)
                    }
                </Switch>
            </Router>
        </LoginContext.Provider>
    );
}

export default App;
