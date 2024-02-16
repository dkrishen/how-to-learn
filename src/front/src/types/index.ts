import { Section } from "api/sections/types";
import { Topic } from "api/topics/types";

export interface SectionModalData extends Omit<Section, "id" | "topics"> {
  topics: Topic[];
  searchTopic: string;
}

export interface CreateSectionModalData extends Omit<Section, "id" | "topics"> {
  topics: Topic[];
  searchTopic: string;
}
