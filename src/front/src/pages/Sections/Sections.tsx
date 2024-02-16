import React, { useEffect, useState } from "react";
import { Grid, Typography } from "@mui/material";

import { Section } from "api/sections/types";

import { useLazyGetSectionQuery, useGetSectionsQuery } from "api/sections";

import useSlicePagination from "hooks/slicePagination";

import GridItem from "components/molecules/GridItem";

import AddRoundedIcon from "@mui/icons-material/AddRounded";
import SectionModal from "components/molecules/SectionModal";
import CreateSectionModal from "components/molecules/CreateSectionModal";

const Sections = () => {
  const [page, setPage] = useState(0);

  const { data, refetch, isLoading } = useGetSectionsQuery({ page });

  const [sectionId, setSectionId] = useState("");
  const [openSectionModal, setOpenSectionModal] = useState<{
    mode: "view" | "create";
    open: boolean;
  }>({
    mode: "view",
    open: false,
  });

  useEffect(() => {
    refetch();
  }, [refetch]);

  const [sections, setSections] = useState<Section[]>([]);

  useEffect(() => {
    setSections((prev) => {
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

  const [updatedSectionId, setUpdatedSectionId] = useState("");
  const [trigger, updatedSection] = useLazyGetSectionQuery();

  useEffect(() => {
    updatedSectionId && trigger(updatedSectionId);
  }, [updatedSectionId, trigger]);

  useEffect(() => {
    if (updatedSection.data) {
      setSections((prev) =>
        prev.map((section) =>
          section.id === updatedSection.data?.id ? updatedSection.data : section
        )
      );
      setUpdatedSectionId("");
    }
  }, [updatedSection]);

  const handleRefetchSections = () => {
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
            setOpenSectionModal({
              mode: "create",
              open: true,
            })
          }
          key={"add_new_section"}
        >
          {<AddRoundedIcon />}
        </GridItem>

        {sections.map((section, index) => {
          return index + 1 === sections.length ? (
            <GridItem
              onClick={() => {
                setOpenSectionModal({
                  mode: "view",
                  open: true,
                });
                setSectionId(section.id);
              }}
              key={section.id}
            >
              <div ref={lastElement} style={{ width: "100%" }}>
                <Typography variant="h6" className="w-full text-center">
                  {section.title}
                </Typography>
              </div>
            </GridItem>
          ) : (
            <GridItem
              onClick={() => {
                setOpenSectionModal({
                  mode: "view",
                  open: true,
                });
                setSectionId(section.id);
              }}
              key={section.id}
            >
              <Typography variant="h6" className="w-full text-center">
                {section.title}
              </Typography>
            </GridItem>
          );
        })}
      </Grid>

      {openSectionModal.open && openSectionModal.mode === "view" && (
        <SectionModal
          open={openSectionModal.open}
          sectionId={sectionId}
          refetchSections={(mode: "update" | "delete") =>
            mode === "update"
              ? setUpdatedSectionId(sectionId)
              : mode === "delete"
                ? setSections((prev) =>
                    prev.filter((section) => section.id !== sectionId)
                  )
                : undefined
          }
          onClose={() => {
            setOpenSectionModal({ mode: "view", open: false });
            setSectionId("");
          }}
        />
      )}

      {openSectionModal.open && openSectionModal.mode === "create" && (
        <CreateSectionModal
          open={openSectionModal.open}
          onClose={() => setOpenSectionModal({ mode: "view", open: false })}
          refetchSections={handleRefetchSections}
        />
      )}
    </>
  );
};

export default Sections;
