import React from 'react';
import { Typography } from '@mui/material';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardContentNoPadding from './CardContentNoPadding';


const MyCard = ({title, description}) => {
    return (
        <Card className='my-card'>
        <CardContentNoPadding>
            <Typography variant="h6" component="div">
              {title}
            </Typography>
            <Typography variant="body2" color="text.secondary">
              {description}
            </Typography>
        </CardContentNoPadding>
        </Card>
    );
};

MyCard.propTypes = {
    
};

export default MyCard;