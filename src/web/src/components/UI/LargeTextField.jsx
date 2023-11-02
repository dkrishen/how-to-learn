import React from 'react';
import { TextField } from '@mui/material';

function LargeTextInput({ label, rows, placeholder }) {
  return (
    <TextField
      id="description-input"
      label={label}
      multiline
      rows={rows}
      placeholder={placeholder}
      className="description-input"
    />
  );
}

export default LargeTextInput;