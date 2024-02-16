import React, { FC, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { Box, List, ListItem, TextField, Typography } from "@mui/material";

import {
  useDeleteSectionMutation,
  useGetSectionQuery,
  useUpdateSectionMutation,
} from "api/sections";
import { useGetTopicsBySectionQuery, useGetTopicsQuery } from "api/topics";

import { SectionModalData } from "types";

import TopicModal from "./TopicModal";

import AutocompliteWithSearch from "./AutocompliteWithSearch";

import ModalContainer from "components/templates/ModalContainer";

interface Props {
  open: boolean;
  onClose: any;
  sectionId: string;
  refetchSections: (mode: "update" | "delete") => void;
}

const MAX_COUNT_OF_DESCRIPTION_CHARACTERS = 100;

const SectionModal: FC<Props> = ({
  open,
  sectionId,
  refetchSections,
  onClose,
}) => {
  const { t } = useTranslation();

  const [updateSection] = useUpdateSectionMutation();
  const [deleteSection] = useDeleteSectionMutation();

  const [savedData, setSavedData] = useState<SectionModalData>({
    title: "",
    topics: [],
    searchTopic: "",
  });
  const [data, setData] = useState<SectionModalData>({
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

  const { data: section, refetch: refetchSection } =
    useGetSectionQuery(sectionId);
  const { data: topicsBySection, refetch: refetchTopicsBySection } =
    useGetTopicsBySectionQuery(sectionId);

  useEffect(() => {
    refetchSection();
    refetchTopicsBySection();
  }, [refetchSection, refetchTopicsBySection]);

  useEffect(() => {
    if (section && topicsBySection) {
      setSavedData({ ...section, topics: topicsBySection, searchTopic: "" });
    }
  }, [section, topicsBySection]);

  const [topicId, setTopicId] = useState("");
  const [openTopicModal, setOpenTopicModal] = useState<boolean>(false);

  const [editMode, setEditMode] = useState(false);

  const [formFieldError, setFormFieldError] = useState({ title: false });

  useEffect(() => {
    setData(savedData);
  }, [savedData]);

  const handleDeleteSection = async () => {
    try {
      await deleteSection({ id: sectionId });
      refetchSections("delete");
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
      if (!data.title) {
        setFormFieldError((prev) => ({ ...prev, title: !data.title }));
        return;
      }

      await updateSection({
        ...data,
        id: sectionId,
        topics: data.topics.map((topic) => topic.id),
      });
      setEditMode(false);
      refetchSection();
      refetchTopicsBySection();
      refetchSections("update");
    } catch (e) {
      console.log(e);
    }
  };

  const handleCancelChanges = () => {
    setEditMode(false);
    setData(savedData);
  };

  return (
    <>
      <ModalContainer
        open={open}
        onClose={onClose}
        title={
          data.title
        }
        onEdit={() => setEditMode((prev) => !prev)}
        onDelete={handleDeleteSection}
        onSaveChanges={handleSaveChanges}
        onCancel={handleCancelChanges}
        editMode={editMode}
      >
        {data && (
          <Box className="flex flex-col w-full h-full gap-2">
            {!editMode ? (
              <>
                <List disablePadding className="flex flex-col gap-2 pl-1">
                  {data.topics.map((topic) => (
                    <ListItem
                      disablePadding
                      className="flex flex-row gap-1 rounded p-1 [&:hover]:bg-[#F4F6FB] cursor-pointer"
                      onClick={() => {
                        setTopicId(topic.id);
                        setOpenTopicModal(true);
                      }}
                      key={topic.id}
                    >
                      <Box className="flex flex-col w-full">
                        <Typography className="w-full font-medium text-sm">
                          {topic.title}
                        </Typography>
                        <Typography className="w-full font-normal text-xs text-[#7f7f7f] text-justify">
                          {topic.description.length <=
                          MAX_COUNT_OF_DESCRIPTION_CHARACTERS
                            ? topic.description
                            : topic.description.slice(0, 100) + "..."}
                        </Typography>
                      </Box>
                    </ListItem>
                  ))}
                </List>
              </>
            ) : (
              <>
                <TextField
                  multiline
                  minRows={1}
                  maxRows={3}
                  placeholder={t("placeholder:enter_the_title_field")}
                  value={data.title}
                  onChange={(e) =>
                    handleChangeFormField("title", e.target.value)
                  }
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
                  getRenderOption={(option) => (
                    <Typography>{option.title}</Typography>
                  )}
                />
              </>
            )}
          </Box>
        )}
      </ModalContainer>
      {openTopicModal && (
        <TopicModal
          open={openTopicModal}
          topicId={topicId}
          onClose={() => {
            setOpenTopicModal(false);
            setTopicId("");
          }}
          refetchTopics={refetchTopicsBySection}
        />
      )}
    </>
  );
};
export default SectionModal;
