import React from 'react';
import {Navigate, Route, Routes} from "react-router-dom";
import {AppRoutes} from "../../router/AppRoutes.js";

const AppRouter = () => {
    return (
            <Routes>
                {AppRoutes.map(route =>
                    <Route
                        Component={route.component}
                        path={route.path}
                        key={route.path}
                        exact={route.exact}
                    />
                )}
                <Route path='*' element={<Navigate to={'/'}/>}  />
            </Routes>
    );
};

export default AppRouter;