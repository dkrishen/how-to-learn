import React from 'react';
import { Box, Button } from '@mui/material';
import LargeTextField from './LargeTextField';

function TextInputForm({ fieldLabel, fieldRowsCount, fieldPlaceholder, buttonAlignment, buttonText }) {
  return (
    <form>
      <Box display="flex" flexDirection="column" alignItems="center">
        <LargeTextField
          label={fieldLabel}
          rows={fieldRowsCount}
          placeholder={fieldPlaceholder}
        />
        <Box display="flex" justifyContent={buttonAlignment} mt={2} width="100%">
          <Button type="submit" variant="outlined" sx={{ width: '40%', minWidth: '80px' }}>
            {buttonText}
          </Button>
        </Box>
      </Box>
    </form>
  );
}

export default TextInputForm;