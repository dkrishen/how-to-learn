import React from "react";
import ReactDOM from "react-dom/client";
import App from "app/App";
import "./index.css";
import { StyledEngineProvider } from "@mui/material";
import { Provider } from "react-redux";
import { store } from "app/store";
import CustomRouter from "components/atoms/CustomRouter";
import { history } from "constant";
import "translations/i18n";

ReactDOM.createRoot(document.getElementById("root")!).render(
  <React.StrictMode>
    <StyledEngineProvider injectFirst>
      <Provider store={store}>
        <CustomRouter history={history} basename="/">
          <App />
        </CustomRouter>
      </Provider>
    </StyledEngineProvider>
  </React.StrictMode>
);
