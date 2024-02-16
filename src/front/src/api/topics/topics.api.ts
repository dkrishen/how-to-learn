import { createApi } from "@reduxjs/toolkit/dist/query/react";
import { baseQuery } from "api";
import { GetTopicsQueries, Topic, TopicsWithPagination } from "./types";

export const topicsApi = createApi({
  reducerPath: "topics/api",
  baseQuery,
  endpoints: (build) => ({
    getTopics: build.query<TopicsWithPagination, GetTopicsQueries | void>({
      query: (topicsQueries) => ({
        url: `/api/topic?${new URLSearchParams(topicsQueries ? (topicsQueries as Record<string, string>) : undefined).toString()}`,
      }),
    }),
    getTopic: build.query<Topic | null, string>({
      query: (id) => ({ url: `/api/topic/${id}` }),
    }),
    getTopicsBySection: build.query<Topic[], string>({
      query: (sectionId) => ({ url: `/api/topic/by-section/${sectionId}` }),
    }),
    createTopic: build.mutation<string, Omit<Topic, "id">>({
      query: (body) => ({ url: "/api/topic", method: "POST", body }),
    }),
    updateTopic: build.mutation<string, Topic>({
      query: (body) => ({ url: "/api/topic", method: "PUT", body }),
    }),
    deleteTopic: build.mutation<string, Pick<Topic, "id">>({
      query: (body) => ({ url: "/api/topic", method: "DELETE", body }),
    }),
  }),
});

export const {
  useGetTopicsQuery,
  useGetTopicQuery,
  useLazyGetTopicQuery,
  useGetTopicsBySectionQuery,
  useCreateTopicMutation,
  useUpdateTopicMutation,
  useDeleteTopicMutation,
} = topicsApi;

export default topicsApi;
