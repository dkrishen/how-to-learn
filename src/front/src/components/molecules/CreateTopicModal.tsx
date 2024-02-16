import React, { FC, useState } from "react";
import { useTranslation } from "react-i18next";
import { TextField } from "@mui/material";

import { useCreateTopicMutation } from "api/topics";

import { Topic } from "api/topics/types";
import ModalContainer from "components/templates/ModalContainer";

interface Props {
  open: boolean;
  onClose: any;
  refetchTopics: any;
}

const CreateTopicModal: FC<Props> = ({ open, onClose, refetchTopics }) => {
  const { t } = useTranslation();

  const [createTopic] = useCreateTopicMutation();

  const [data, setData] = useState<Omit<Topic, "id">>({
    title: "",
    description: "",
  });

  const [formFieldError, setFormFieldError] = useState({
    title: false,
    description: false,
  });

  const handleChangeFormField = (key: string, newValue: string) => {
    setFormFieldError((prev) => ({ ...prev, [key]: false }));
    setData((prev) => ({ ...prev, [key]: newValue }));
  };

  const handleCreate = async () => {
    try {
      if (!data.title || !data.description) {
        setFormFieldError((prev) => ({
          ...prev,
          title: !data.title,
          description: !data.description,
        }));
        return;
      }

      await createTopic(data);
      refetchTopics();
      onClose();
    } catch (e) {
      console.log(e);
    }
  };

  return (
    <ModalContainer
      open={open}
      onClose={onClose}
      title={t("title:topic_modal_create_mode")}
      onCreate={handleCreate}
      onCancel={onClose}
    >
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
        onChange={(e) => handleChangeFormField("description", e.target.value)}
        error={formFieldError.description}
      />
    </ModalContainer>
  );
};

export default CreateTopicModal;
