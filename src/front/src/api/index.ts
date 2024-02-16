import { fetchBaseQuery } from "@reduxjs/toolkit/query/react";
import { BASE_URL } from "../constant";

export const baseQuery = fetchBaseQuery({
  baseUrl: BASE_URL,
  credentials: "same-origin",
  prepareHeaders: (headers) => {
    return headers;
  },
});
