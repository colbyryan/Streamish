import React from "react";
import VideoList from "./VideoList";
import Login from "./Login";
import Register from "./Register";
import { Switch, Route, Redirect } from "react-router-dom";
import Favorites from "./Favorites";


export default function ApplicationViews({ isLoggedIn }) {
    return (
        <main>
            <Switch>
                <Route path="/" exact>
                    {isLoggedIn ? <VideoList /> : <Redirect to="/login" />}
                </Route>
                <Route path="/test" exact>
                    {isLoggedIn ? <Favorites /> : <Redirect to="/login" />}
                </Route>
                <Route path="/login">
                    <Login />
                </Route>

                <Route path="/register">
                    <Register />
                </Route>
            </Switch>
        </main>
    );
};