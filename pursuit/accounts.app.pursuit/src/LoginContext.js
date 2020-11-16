import React, { useContext } from "react";

export const LoginContext = React.createContext({
    loggedIn: false,
    accessToken: ""
})

export function useLoginContext() {
    return useContext(LoginContext);
}