import React, { FC } from "react";
import {
  Box,
  Button,
  Fade,
  IconButton,
  Modal,
  Typography,
} from "@mui/material";
import { useTranslation } from "react-i18next";

import CloseRoundedIcon from "@mui/icons-material/CloseRounded";
import EditRoundedIcon from "@mui/icons-material/EditRounded";
import DeleteOutlineRoundedIcon from "@mui/icons-material/DeleteOutlineRounded";

interface Props {
  open: boolean;
  title?: string;
  editMode?: boolean;
  onClose?: any;
  onEdit?: any;
  onDelete?: any;
  onCreate?: any;
  onCancel?: any;
  onSaveChanges?: any;
  children: React.ReactNode;
}

const ModalContainer: FC<Props> = ({
  open,
  title,
  editMode,
  onClose,
  onEdit,
  onDelete,
  onCreate,
  onCancel,
  onSaveChanges,
  children,
}) => {
  const { t } = useTranslation();
  return (
    <Modal
      open={open}
      onClose={onClose}
      closeAfterTransition
      disableAutoFocus
      BackdropProps={{
        style: {
          backgroundColor: "#778094",
          opacity: 0.25,
        },
      }}
    >
      <Fade in={open}>
        <Box
          className="absolute top-1/2 left-1/2 p-4 flex flex-col gap-4 rounded"
          sx={{
            transform: "translate(-50%, -50%)",
            bgcolor: "background.paper",
            maxHeight: "90%",
            width: "75%",
          }}
        >
          <Box className="flex items-center justify-between">
            {title && (
              <Typography variant="h6" className="font-medium text-2xl">
                {title}
              </Typography>
            )}
            <Box>
              <Box className="flex flex-row gap-5">
                {(onEdit || onDelete) && (
                  <Box className="flex flex-row gap-1">
                    {onEdit && !editMode && (
                      <IconButton onClick={onEdit}>
                        <EditRoundedIcon />
                      </IconButton>
                    )}
                    {onEdit && onSaveChanges && onCancel && editMode && (
                      <>
                        <Button onClick={onSaveChanges}>
                          {t("button:save_changes")}
                        </Button>
                        <Button onClick={onCancel} color="error">
                          {t("button:cancel")}
                        </Button>
                      </>
                    )}
                    {onDelete && !editMode && (
                      <IconButton color="error" onClick={onDelete}>
                        <DeleteOutlineRoundedIcon />
                      </IconButton>
                    )}
                  </Box>
                )}

                {onCreate && onCancel && (
                  <>
                    <Button onClick={onCreate}>{t("button:create")}</Button>
                    <Button onClick={onCancel}>{t("button:cancel")}</Button>
                  </>
                )}

                <IconButton onClick={onClose}>
                  <CloseRoundedIcon />
                </IconButton>
              </Box>
            </Box>
          </Box>

          {children}
        </Box>
      </Fade>
    </Modal>
  );
};

export default ModalContainer;
