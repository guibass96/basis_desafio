  export const calculateTableHeight = (rowsPerPage:any,data:any) => {
    const rowHeight = 53; 
    const headerHeight = 57; 
    const paginationHeight = 52; 
    
    const visibleRows = Math.max(2, Math.min(rowsPerPage, data.length));
    
    return headerHeight + (visibleRows * rowHeight) + paginationHeight;
  };