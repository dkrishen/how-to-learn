import React, { FC, useEffect, useState } from "react";
import {
  Autocomplete,
  Box,
  Chip,
  ClickAwayListener,
  Icon,
  List,
  ListItem,
  Paper,
  Popper,
  TextField,
  Typography,
} from "@mui/material";

import CloseRoundedIcon from "@mui/icons-material/CloseRounded";
import useDebounce from "hooks/debounce";
import { useTranslation } from "react-i18next";

interface Props {
  options: any[] | undefined;
  loading?: boolean;
  selectedOptions: any[];
  inputValue: string;
  placeholder?: string;
  onChangeInputValue: (value: string) => void;
  saveOptions: (selectedOptions: any[]) => void;
  getValue: (options: any) => any;
  getRenderOption: (option: any) => React.ReactNode;
}

const AutocompliteWithSearch: FC<Props> = ({
  options,
  loading,
  selectedOptions,
  inputValue,
  placeholder,
  onChangeInputValue,
  saveOptions,
  getValue,
  getRenderOption,
}) => {
  const { t } = useTranslation();

  const [search, setSearch] = useState(inputValue);
  const debouncedSearch = useDebounce(search, 500);

  useEffect(() => {
    if (debouncedSearch !== inputValue) {
      onChangeInputValue(debouncedSearch);
    }
  }, [debouncedSearch]);

  const [selectedLocalOptions, setSelectedLocalOptions] =
    useState<any[]>(selectedOptions);

  useEffect(() => {
    if (
      !(
        selectedOptions.length === selectedLocalOptions.length &&
        selectedOptions.every((val, index) => {
          return Object.keys(val).reduce(
            (prev, key) =>
              prev && val[key] === selectedLocalOptions[index][key],
            true
          );
        })
      )
    ) {
      setSelectedLocalOptions(selectedOptions);
    }
  }, [selectedOptions]);

  const [open, setOpen] = useState(false);

  useEffect(() => {
    if (options && !loading) {
      options.length || search ? setOpen(true) : setOpen(false);
    } else {
      setOpen(false);
    }
  }, [options, search, loading]);

  useEffect(() => {
    if (
      !(
        selectedOptions.length === selectedLocalOptions.length &&
        selectedOptions.every((val, index) => {
          return Object.keys(val).reduce(
            (prev, key) =>
              prev && val[key] === selectedLocalOptions[index][key],
            true
          );
        })
      )
    ) {
      saveOptions(selectedLocalOptions);
    }
  }, [selectedLocalOptions]);

  const handleDelete = (item: string) => {
    setSelectedLocalOptions((prev) =>
      prev.filter((selectedOption) => getValue(selectedOption) !== item)
    );
  };

  const handleClickOption = (option: any) => {
    setSelectedLocalOptions((prev) => {
      return prev.find((item) => getValue(item) === getValue(option))
        ? prev.filter((item) => getValue(item) !== getValue(option))
        : [...prev, option];
    });
    setSearch("");
  };

  return (
    <Autocomplete
      multiple
      options={[]}
      freeSolo
      value={selectedOptions.map((option) => getValue(option))}
      renderTags={(value) => {
        return value.map((option) => (
          <Chip
            variant="outlined"
            label={option}
            deleteIcon={
              <Icon>
                <CloseRoundedIcon />
              </Icon>
            }
            onDelete={() => handleDelete(option)}
            key={option}
          />
        ));
      }}
      onInputChange={(e, newValue) => setSearch(newValue)}
      inputValue={search}
      renderInput={(params) => (
        <TextField
          {...params}
          onClick={() => setOpen(true)}
          variant="outlined"
          placeholder={placeholder}
          InputProps={{
            ...params.InputProps,
            style: {
              padding: "7px 12px 7px 12px",
              gap: "10px",
            },
            startAdornment: <>{params.InputProps.startAdornment}</>,
            endAdornment: null,
          }}
        />
      )}
      open={open}
      PopperComponent={(props) => (
        <ClickAwayListener onClickAway={() => setOpen(false)}>
          <Popper {...props}>
            <Paper className="mt-2">
              <List disablePadding className="max-h-[156px] overflow-auto">
                {options?.length && search ? (
                  options.map((option) => (
                    <ListItem
                      disablePadding
                      sx={{ margin: 0, padding: "4px" }}
                      key={getValue(option)}
                    >
                      <Box
                        className={`w-full h-9 rounded px-0.5 flex flex-row gap-2.5 items-center hover:bg-[#F4F6FB] cursor-pointer ${
                          selectedOptions.find(
                            (item) => getValue(item) === getValue(option)
                          )
                            ? "bg-[#F4F6FB]"
                            : ""
                        }`}
                        onClick={() => handleClickOption(option)}
                      >
                        {getRenderOption(option)}
                      </Box>
                    </ListItem>
                  ))
                ) : search ? (
                  <ListItem
                    disablePadding
                    sx={{ margin: 0, padding: "4px" }}
                    key="no_options"
                  >
                    <Box className="w-full h-9 flex flex-row gap-2.5 items-center cursor-default">
                      <Typography>{t("text:no_options")}</Typography>
                    </Box>
                  </ListItem>
                ) : null}
              </List>
            </Paper>
          </Popper>
        </ClickAwayListener>
      )}
    />
  );
};

export default AutocompliteWithSearch;
