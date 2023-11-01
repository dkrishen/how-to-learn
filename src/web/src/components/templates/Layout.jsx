import React from 'react';
import Header from '../UI/Header';
import { Box } from '@mui/material';
import '../../styles/App.css'

const Layout = props => {
    return (
        <Box className="central-box">
            <Header/>
            <Box className="under-header ">
                    {props.children}
            </Box>
        </Box>
    );
};

export default Layout;