import { useCallback, useRef } from "react";

export default function useSlicePagination(
  isLoading: boolean,
  isLast: boolean,
  setPage: React.Dispatch<React.SetStateAction<number>>
) {
  const observer = useRef<IntersectionObserver>();
  const lastElement = useCallback(
    (node: HTMLDivElement) => {
      if (isLoading) return;
      if (observer.current) observer.current.disconnect();
      observer.current = new IntersectionObserver((entries) => {
        if (entries[0].isIntersecting && !isLast) {
          setPage((prev) => prev + 1);
        }
      });
      if (node) observer.current.observe(node);
    },
    [isLoading, isLast]
  );

  return lastElement;
}
