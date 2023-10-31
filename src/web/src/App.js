import React, { useState } from 'react';
import './styles/App.css';
import { BrowserRouter } from 'react-router-dom';
import AppRouter from "./components/containers/AppRouter";
import Layout from './components/templates/Layout';

function App() {

  return (
    <BrowserRouter>
      <Layout>
        <AppRouter/>
      </Layout>
    </BrowserRouter>
  );
}

export default App;