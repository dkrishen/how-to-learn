import { Box, Button, TextField } from '@mui/material';
import React from 'react';
import { Form } from 'react-router-dom';
import LargeTextInput from '../components/UI/LargeTextField';
import TextInputForm from '../components/UI/TextInputForm';

const Main = () => {
    return (
        <>
            <TextInputForm
                fieldLabel="Description of project"
                fieldRowsCount={12}
                fieldPlaceholder="Describe the project in free form in Russian or English"
                buttonAlignment="center"
                buttonText="Generate"
            />
        </>
    );
};

export default Main;