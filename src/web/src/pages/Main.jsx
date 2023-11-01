import { Box, Button, TextField } from '@mui/material';
import React from 'react';
import { Form } from 'react-router-dom';

const Main = () => {
    return (
        <>
        <form>
            <Box display="flex" flexDirection="column" alignItems="center">
                <TextField 
                    id="description-input"
                    label="Description of project"
                    multiline
                    rows={12}
                    placeholder='Describe the project in free form in Russian or English'
                    className='description-input'
                    />
                <Box display="flex" justifyContent="center" mt={2} width="100%">
                    <Button type='submit' variant="outlined" sx={{width: '40%'}}>
                        Generate
                    </Button>
                </Box>
            </Box>
        </form>
        </>
    );
};

export default Main;