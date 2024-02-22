import { Topic } from "api/topics/types";

export interface Section {
  id: string;
  title: string;
  topics: string[];
}

export interface SectionByDataResponse extends Omit<Section,'topics'>{
  topics:Topic[]
}

export interface SectionsWithPagination {
  items: Section[];
  last: boolean;
}

export interface GetSectionsQueries {
  page?: number;
  size?: number;
}

export interface SectionsByDataBody  {
  description: string
}