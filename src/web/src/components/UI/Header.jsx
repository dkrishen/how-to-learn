import React from 'react';
import { AppBar, IconButton, Toolbar } from '@mui/material';
import EditIcon from '@mui/icons-material/Edit';
import SchoolIcon from '@mui/icons-material/School';
import Typography from '@mui/material/Typography';
import { useNavigate } from 'react-router-dom';

const Header = props => {
    const navigate = useNavigate();

    const handleMainPage = () => {
        navigate('/');
      };

    const handleSectionsPage = () => {
        navigate('/sections');
      };

    return (
        <AppBar>
            <Toolbar sx={{ display: 'flex', justifyContent: 'space-between' }}>
                <IconButton color="inherit" onClick={handleMainPage}>
                    <SchoolIcon  sx={{mr: 2}} />
                    <Typography variant="h6" component="div">
                        How to learn
                    </Typography>
                </IconButton>
                <IconButton onClick={handleSectionsPage} color="inherit">
                    <EditIcon />
                </IconButton>
            </Toolbar>
        </AppBar>
    );
};

export default Header;