import React, { FC } from "react";
import { Grid, Paper } from "@mui/material";

interface Props {
  onClick?: React.MouseEventHandler<HTMLDivElement>;
  children: React.ReactNode;
}

const GridItem: FC<Props> = ({ onClick, children }) => {
  return (
    <Grid item xs={6} sm={6} md={4} lg={3} xl={2}>
      <Paper
        className="flex h-full w-full justify-center items-center min-h-[60px] cursor-pointer"
        onClick={onClick}
      >
        {children}
      </Paper>
    </Grid>
  );
};

export default GridItem;
