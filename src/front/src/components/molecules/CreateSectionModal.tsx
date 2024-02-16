import React, { FC, useState } from "react";
import { useTranslation } from "react-i18next";
import { TextField, Typography } from "@mui/material";

import { useCreateSectionMutation } from "api/sections";
import { useGetTopicsQuery } from "api/topics";

import { CreateSectionModalData } from "types";

import ModalContainer from "components/templates/ModalContainer";

import AutocompliteWithSearch from "./AutocompliteWithSearch";

interface Props {
  open: boolean;
  onClose: any;
  refetchSections: any;
}

const CreateSectionModal: FC<Props> = ({ open, onClose, refetchSections }) => {
  const { t } = useTranslation();

  const [createSection] = useCreateSectionMutation();

  const [data, setData] = useState<CreateSectionModalData>({
    title: "",
    topics: [],
    searchTopic: "",
  });

  const { data: topicsByPattern, isLoading } = useGetTopicsQuery(
    {
      pattern: data.searchTopic,
    },
    { skip: !data.searchTopic }
  );

  const [formFieldError, setFormFieldError] = useState({ title: false });

  const handleChangeFormField = (key: string, newValue: string) => {
    setFormFieldError((prev) => ({ ...prev, [key]: false }));
    setData((prev) => ({ ...prev, [key]: newValue }));
  };

  const handleCreate = async () => {
    try {
      if (!data.title) {
        setFormFieldError((prev) => ({ ...prev, title: !data.title }));
        return;
      }

      await createSection({
        ...data,
        topics: data.topics.map((topic) => topic.id),
      });
      refetchSections();
      onClose();
    } catch (e) {
      console.log(e);
    }
  };

  return (
    <ModalContainer
      open={open}
      onClose={onClose}
      title={t("title:section_modal_create_mode")}
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

      <AutocompliteWithSearch
        options={topicsByPattern?.items}
        loading={isLoading}
        selectedOptions={data.topics}
        placeholder={t("placeholder:select_topics")}
        inputValue={data.searchTopic}
        onChangeInputValue={(value) =>
          setData((prev) => ({ ...prev, searchTopic: value }))
        }
        saveOptions={(topics) => {
          setData((prev) => ({ ...prev, topics }));
        }}
        getValue={(option) => option.title}
        getRenderOption={(option) => <Typography>{option.title}</Typography>}
      />
    </ModalContainer>
  );
};

export default CreateSectionModal;
