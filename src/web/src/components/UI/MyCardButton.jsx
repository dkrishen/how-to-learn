import React from 'react';
import { Typography } from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardContentNoPadding from './CardContentNoPadding';

const MyCardButton = ({}) => {
    return (
      <Card className='my-card my-card-button'>
      <CardContentNoPadding>
            <AddIcon sx={{fontSize: 'large', color: '#999'}}/>
        </CardContentNoPadding>
        </Card>
    );
};

MyCardButton.propTypes = {
    
};

export default MyCardButton;