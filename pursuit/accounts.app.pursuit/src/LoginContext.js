import React, { useContext } from "react";

export const LoginContext = React.createContext({
    loggedIn: false
})

export function useLoginContext() {
    return useContext(LoginContext);
}