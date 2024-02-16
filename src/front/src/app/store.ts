import { configureStore } from "@reduxjs/toolkit";
import { sectionsApi } from "api/sections";
import { topicsApi } from "api/topics";

export const store = configureStore({
  reducer: {
    [sectionsApi.reducerPath]: sectionsApi.reducer,
    [topicsApi.reducerPath]: topicsApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(sectionsApi.middleware, topicsApi.middleware),
});

export type RootState = ReturnType<typeof store.getState>;
