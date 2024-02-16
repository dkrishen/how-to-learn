export interface GetTopicsQueries {
  pattern?: string;
  page?: number;
  size?: number;
}

export interface Topic {
  id: string;
  title: string;
  description: string;
}

export interface TopicsWithPagination {
  items: Topic[];
  last: boolean;
}
