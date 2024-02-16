import React, { FC, ReactNode } from "react";
import {
  BottomNavigation,
  BottomNavigationAction,
  Box,
  Paper,
} from "@mui/material";

import { paths } from "app/constant";
import { useLocation, useNavigate } from "react-router-dom";

import HomeRoundedIcon from "@mui/icons-material/HomeRounded";
import TopicRoundedIcon from "@mui/icons-material/TopicRounded";
import LibraryBooksRoundedIcon from "@mui/icons-material/LibraryBooksRounded";

interface Props {
  children: ReactNode;
}

const Layout: FC<Props> = ({ children }: any) => {
  const { pathname } = useLocation();
  const navigate = useNavigate();

  return (
    <Box className="flex w-full h-full">
      <Box
        sx={{
          position: "relative",
          width: "100%",
          height: `calc(100vh - 56px)`,
          overflow: "auto",
          backgroundColor: "#F4F6FB",
        }}
      >
        {children}
      </Box>
      <Paper className={"fixed bottom-0 inset-x-0 h-[56px]"} elevation={3}>
        <BottomNavigation
          showLabels
          value={pathname}
          onChange={(event, newValue) => {
            navigate(newValue);
          }}
          className="h-full"
        >
          <BottomNavigationAction
            icon={<HomeRoundedIcon />}
            value={paths.generatePlan}
          />
          <BottomNavigationAction
            icon={<TopicRoundedIcon />}
            value={paths.sections}
          />
          <BottomNavigationAction
            icon={<LibraryBooksRoundedIcon />}
            value={paths.topics}
          />
        </BottomNavigation>
      </Paper>
    </Box>
  );
};

export default Layout;
