export interface Section {
  id: string;
  title: string;
  topics: string[];
}

export interface SectionsWithPagination {
  items: Section[];
  last: boolean;
}

export interface GetSectionsQueries {
  pattern?: string;
  page?: number;
  size?: number;
}
