import React, { FC, useEffect, useState } from "react";
import { Box, TextField, Typography } from "@mui/material";
import { useTranslation } from "react-i18next";

import ModalContainer from "components/templates/ModalContainer";
import {
  useDeleteTopicMutation,
  useGetTopicQuery,
  useUpdateTopicMutation,
} from "api/topics";
import { Topic } from "api/topics/types";

interface Props {
  open: boolean;
  onClose: any;
  topicId: string;
  refetchTopics: (mode: "update" | "delete") => void;
}

const TopicModal: FC<Props> = ({ open, topicId, onClose, refetchTopics }) => {
  const { t } = useTranslation();

  const [updateTopic] = useUpdateTopicMutation();
  const [deleteTopic] = useDeleteTopicMutation();

  const [savedData, setSavedData] = useState<Omit<Topic, "id">>({
    title: "",
    description: "",
  });

  const [data, setData] = useState<Omit<Topic, "id">>({
    title: "",
    description: "",
  });

  const { data: topic, refetch: refetchTopic } = useGetTopicQuery(topicId);

  useEffect(() => {
    refetchTopic();
  }, [refetchTopic]);

  useEffect(() => {
    topic && setSavedData(topic);
  }, [topic]);

  const [editMode, setEditMode] = useState(false);

  const [formFieldError, setFormFieldError] = useState({
    title: false,
    description: false,
  });

  useEffect(() => {
    setData(savedData);
  }, [savedData]);

  const handleDeleteTopic = async () => {
    try {
      await deleteTopic({ id: topicId });
      refetchTopics("delete");
      onClose();
    } catch (e) {
      console.log(e);
    }
  };

  const handleChangeFormField = (key: string, newValue: string) => {
    setFormFieldError((prev) => ({ ...prev, [key]: false }));
    setData((prev) => ({ ...prev, [key]: newValue }));
  };

  const handleSaveChanges = async () => {
    try {
      if (!data.title || !data.description) {
        setFormFieldError((prev) => ({
          ...prev,
          title: !data.title,
          description: !data.description,
        }));
        return;
      }

      await updateTopic({ ...data, id: topicId });
      setEditMode(false);
      refetchTopic();
      refetchTopics("update");
    } catch (e) {
      console.log(e);
    }
  };

  const handleCancelChanges = () => {
    setEditMode(false);
    setData(savedData);
  };

  return (
    <ModalContainer
      open={open}
      onClose={onClose}
      title={
        data.title
      }
      onEdit={() => setEditMode((prev) => !prev)}
      onDelete={handleDeleteTopic}
      onSaveChanges={handleSaveChanges}
      onCancel={handleCancelChanges}
      editMode={editMode}
    >
      {data && (
        <Box className="flex flex-col w-full h-full gap-2">
          {!editMode ? (
            <>
              <Typography className="font-medium text-sm text-justify">
                {data.description}
              </Typography>
            </>
          ) : (
            <>
              <TextField
                multiline
                minRows={1}
                maxRows={3}
                placeholder={t("placeholder:enter_the_title_field")}
                value={data.title}
                onChange={(e) => handleChangeFormField("title", e.target.value)}
                error={formFieldError.title}
              />
              <TextField
                multiline
                minRows={3}
                maxRows={5}
                placeholder={t("placeholder:enter_the_description_field")}
                value={data.description}
                onChange={(e) =>
                  handleChangeFormField("description", e.target.value)
                }
                error={formFieldError.description}
              />
            </>
          )}
        </Box>
      )}
    </ModalContainer>
  );
};

export default TopicModal;
