import React, { useState } from "react";
import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Box,
  Button,
  List,
  ListItem,
  TextField,
  Typography,
} from "@mui/material";
import { useTranslation } from "react-i18next";

import { useLazyGetSectionsByDataQuery } from "api/sections";
import { useGetTopicsBySectionQuery } from "api/topics";

import ExpandMoreRoundedIcon from "@mui/icons-material/ExpandMoreRounded";
import TopicModal from "components/molecules/TopicModal";

const GeneratePlan = () => {
  const { t } = useTranslation();

  const [dataForGeneration, setDataForGeneration] = useState("");
  const [dataForGenerationError, setDataForGenerationError] = useState(false);

  const [triggerGetSections, { data: sections }] =
    useLazyGetSectionsByDataQuery();

  const [selectedSectionId, setSelectedSection] = useState("");

  const { data: topics, refetch: refetchTopics } = useGetTopicsBySectionQuery(
    selectedSectionId,
    {
      skip: !selectedSectionId,
    }
  );

  const [topicId, setTopicId] = useState("");

  const handleClickGenerateButton = () => {
    try {
      setDataForGenerationError(!dataForGeneration);
      if (dataForGeneration) {
        triggerGetSections(encodeURIComponent(JSON.stringify(dataForGeneration)));
      }
    } catch (err) {
      console.error(err);
    }
  };

  const handleClickSection = (sectionId: string) => {
    selectedSectionId === sectionId
      ? setSelectedSection("")
      : setSelectedSection(sectionId);
  };

  return (
    <>
      <Box className="w-full h-full flex flex-col gap-16 p-10">
        <Box className="w-full flex flex-col gap-10">
          <Typography variant="h3" className="w-full text-center">
            {t("text:get_scientific_information_on_input_data")}
          </Typography>

          <TextField
            multiline
            rows={10}
            value={dataForGeneration}
            error={dataForGenerationError}
            onChange={(e) => {
              setDataForGeneration(e.target.value);
              setDataForGenerationError(false);
            }}
            placeholder={t("placeholder:enter_input_data_for_generation")}
            className="bg-white"
          />

          <Button
            variant="contained"
            className="w-full"
            onClick={handleClickGenerateButton}
          >
            {t("button:generate")}
          </Button>
        </Box>

        <List disablePadding className="flex flex-col gap-2 w-full pb-2">
          {sections?.map((section) => {
            return (
              <ListItem
                disablePadding
                className="w-full min-h-[40px] px-5"
                key={section.id}
              >
                <Accordion
                  className="w-full h-full"
                  expanded={selectedSectionId === section.id}
                >
                  <AccordionSummary
                    expandIcon={<ExpandMoreRoundedIcon />}
                    onClick={() => handleClickSection(section.id)}
                  >
                    <Typography variant="h5">{section.title}</Typography>
                  </AccordionSummary>
                  <AccordionDetails>
                    <List
                      disablePadding
                      className="flex flex-col gap-2 w-full pl-4"
                    >
                      {topics?.map((topic) => {
                        return (
                          <ListItem
                            disablePadding
                            className="flex flex-row gap-1 rounded p-1 [&:hover]:bg-[#F4F6FB] cursor-pointer"
                            key={topic.id}
                            onClick={() => setTopicId(topic.id)}
                          >
                            <Typography variant="h6" className="w-full">
                              {topic.title}
                            </Typography>
                          </ListItem>
                        );
                      })}
                    </List>
                  </AccordionDetails>
                </Accordion>
              </ListItem>
            );
          })}
        </List>
      </Box>

      {topicId ? (
        <TopicModal
          open={!!topicId}
          onClose={() => setTopicId("")}
          topicId={topicId}
          refetchTopics={refetchTopics}
        />
      ) : null}
    </>
  );
};

export default GeneratePlan;
