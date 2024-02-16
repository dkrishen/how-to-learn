import React, { useEffect, useState } from "react";
import { Grid, Typography } from "@mui/material";

import { Topic } from "api/topics/types";

import { useGetTopicsQuery, useLazyGetTopicQuery } from "api/topics";

import useSlicePagination from "hooks/slicePagination";

import GridItem from "components/molecules/GridItem";

import AddRoundedIcon from "@mui/icons-material/AddRounded";
import TopicModal from "components/molecules/TopicModal";
import CreateTopicModal from "components/molecules/CreateTopicModal";

const Topics = () => {
  const [page, setPage] = useState(0);

  const { data, refetch, isLoading } = useGetTopicsQuery({ page });

  const [topicId, setTopicId] = useState("");
  const [openTopicModal, setOpenTopicModal] = useState<{
    mode: "view" | "create";
    open: boolean;
  }>({
    mode: "view",
    open: false,
  });

  useEffect(() => {
    refetch();
  }, [refetch]);

  const [topics, setTopics] = useState<Topic[]>([]);

  useEffect(() => {
    setTopics((prev) => {
      if (data?.items) {
        if (page === 0) {
          return data.items;
        }
        return Object.values({
          ...prev.reduce(
            (prevVal, current) => ({
              ...prevVal,
              ["id" + current.id]: current,
            }),
            {}
          ),
          ...data.items.reduce(
            (prevVal, current) => ({
              ...prevVal,
              ["id" + current.id]: current,
            }),
            {}
          ),
        });
      }
      return prev;
    });
  }, [data, page]);

  const lastElement = useSlicePagination(
    isLoading,
    data?.last || false,
    setPage
  );

  const [updatedTopicId, setUpdatedTopicId] = useState("");
  const [trigger, updatedTopic] = useLazyGetTopicQuery();

  useEffect(() => {
    updatedTopicId && trigger(updatedTopicId);
  }, [updatedTopicId, trigger]);

  useEffect(() => {
    if (updatedTopic.data) {
      setTopics((prev) =>
        prev.map((topic) =>
          topic.id === updatedTopic.data?.id ? updatedTopic.data : topic
        )
      );
      setUpdatedTopicId("");
    }
  }, [updatedTopic]);

  const handleRefetchTopics = () => {
    page !== 0 ? setPage(0) : refetch();
  };

  useEffect(() => {
    refetch();
  }, [page, refetch]);

  return (
    <>
      <Grid container spacing={2} className="p-10">
        <GridItem
          onClick={() =>
            setOpenTopicModal({
              mode: "create",
              open: true,
            })
          }
          key={"add_new_topic"}
        >
          {<AddRoundedIcon />}
        </GridItem>

        {topics.map((topic, index) => {
          return index + 1 === topics.length ? (
            <GridItem
              onClick={() => {
                setOpenTopicModal({
                  mode: "view",
                  open: true,
                });
                setTopicId(topic.id);
              }}
              key={topic.id}
            >
              <div ref={lastElement} style={{ width: "100%" }}>
                <Typography variant="h6" className="w-full text-center">
                  {topic.title}
                </Typography>
              </div>
            </GridItem>
          ) : (
            <GridItem
              onClick={() => {
                setOpenTopicModal({
                  mode: "view",
                  open: true,
                });
                setTopicId(topic.id);
              }}
              key={topic.id}
            >
              <Typography variant="h6" className="w-full text-center">
                {topic.title}
              </Typography>
            </GridItem>
          );
        })}
      </Grid>

      {openTopicModal.open && openTopicModal.mode === "view" && (
        <TopicModal
          open={openTopicModal.open}
          topicId={topicId}
          refetchTopics={(mode: "update" | "delete") =>
            mode === "update"
              ? setUpdatedTopicId(topicId)
              : mode === "delete"
                ? setTopics((prev) =>
                    prev.filter((topic) => topic.id !== topicId)
                  )
                : undefined
          }
          onClose={() => {
            setOpenTopicModal({ mode: "view", open: false });
            setTopicId("");
          }}
        />
      )}

      {openTopicModal.open && openTopicModal.mode === "create" && (
        <CreateTopicModal
          open={openTopicModal.open}
          onClose={() => setOpenTopicModal({ mode: "view", open: false })}
          refetchTopics={handleRefetchTopics}
        />
      )}
    </>
  );
};

export default Topics;
