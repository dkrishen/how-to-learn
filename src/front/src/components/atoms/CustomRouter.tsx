import { FC, ReactNode, useLayoutEffect, useState } from "react";
import { HashRouter } from "react-router-dom";

interface Props {
  history: any;
  children: ReactNode;
  basename: string;
}

const CustomRouter: FC<Props> = ({ history, ...props }) => {
  const [, setState] = useState({
    action: history.action,
    location: history.location,
  });

  useLayoutEffect(() => history.listen(setState), [history]);

  return <HashRouter {...props} />;
};

export default CustomRouter;
