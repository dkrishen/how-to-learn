import { CssBaseline, ThemeProvider } from "@mui/material";
import theme from "./theme";
import { Navigate, Route, Routes } from "react-router-dom";
import { paths } from "./constant";

import Layout from "components/templates";

import GeneratePlan from "pages/GeneratePlan";
import Sections from "pages/Sections";
import Topics from "pages/Topics";

function App() {
  return (
    <>
      <ThemeProvider theme={theme}>
        <CssBaseline />
        <Layout>
          <Routes>
            <Route path={paths.generatePlan} element={<GeneratePlan />} />
            <Route path={paths.sections} element={<Sections />} />
            <Route path={paths.topics} element={<Topics />} />
            <Route
              path="*"
              element={<Navigate to={paths.generatePlan} replace />}
            />
          </Routes>
        </Layout>
      </ThemeProvider>
    </>
  );
}

export default App;
