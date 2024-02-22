import { createApi } from "@reduxjs/toolkit/dist/query/react";
import { baseQuery } from "api";
import { GetSectionsQueries, Section, SectionsWithPagination, SectionByDataResponse, SectionsByDataBody } from "./types";

export const sectionsApi = createApi({
  reducerPath: "sections/api",
  baseQuery,
  endpoints: (build) => ({
    getSections: build.query<SectionsWithPagination, GetSectionsQueries | void>(
      {
        query: (sectionsQueries) => ({
          url: `/api/section?${new URLSearchParams(sectionsQueries ? (sectionsQueries as Record<string, string>) : undefined).toString()}`,
        }),
      }
    ),
    getSection: build.query<Section | null, string>({
      query: (id) => ({ url: `/api/section/${id}` }),
    }),
    getSectionsByData: build.query<SectionByDataResponse[], SectionsByDataBody>({
      query: (body) => ({ url: "/api/section/by-data", method: "POST", body }), // change
    }),
    createSection: build.mutation<string, Omit<Section, "id">>({
      query: (body) => ({ url: "/api/section", method: "POST", body }),
    }),
    updateSection: build.mutation<string, Section>({
      query: (body) => ({ url: "/api/section", method: "PUT", body }),
    }),
    deleteSection: build.mutation<string, Pick<Section, "id">>({
      query: (body) => ({ url: "/api/section", method: "DELETE", body }),
    }),
  }),
});

export const {
  useGetSectionsQuery,
  useGetSectionQuery,
  useLazyGetSectionQuery,
  useGetSectionsByDataQuery,
  useLazyGetSectionsByDataQuery,
  useCreateSectionMutation,
  useUpdateSectionMutation,
  useDeleteSectionMutation,
} = sectionsApi;

export default sectionsApi;
