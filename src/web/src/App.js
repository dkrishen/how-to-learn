import React, { useState } from 'react';
import './styles/App.css';
import { BrowserRouter, Route, Switch } from 'react-router-dom';
import AppRouter from "./components/AppRouter";

function App() {

  return (
    <BrowserRouter>
        <AppRouter/>
    </BrowserRouter>
  );
}

export default App;