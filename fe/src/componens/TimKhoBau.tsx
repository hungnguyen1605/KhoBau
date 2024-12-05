import React, { useEffect, useState } from "react";
import { TextField, Button, Grid, Typography, Snackbar, Alert, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper } from "@mui/material";
import axios from "axios";
import { apiDeletedData, apiGetData } from "../api/TimKhoBau.api";
import ArrowForwardIcon from '@mui/icons-material/ArrowForward';
import _ from 'lodash';
import Card from '@mui/material/Card';
import DeleteIcon from '@mui/icons-material/Delete';
const TreasureForm = () => {
  const [rows, setRows] = useState(0);
  const [columns, setColumns] = useState(0);
  const [maxChest, setMaxChest] = useState(0);
  const [matrix, setMatrix] = useState<number[][]>([]);
  const [message, setMessage] = useState("");
  const [shortestPath, setShortestPath] = useState<number>(0);
  const [history, setHistory] = useState<any[]>([]); // Lưu trữ lịch sử các lần nhập
  const [openSnackbar, setOpenSnackbar] = useState(false);
  const [snackbarSeverity, setSnackbarSeverity] = useState<"success" | "error">("success");

  // Lấy dữ liệu từ API
  useEffect(() => {
    getData()
  }, []);
  const getData = () => {
    apiGetData().then((res: any) => {
      const data = res.data.data.map((x: any) => {
        return {
          ...x,
          matrix: JSON.parse(x.matrix)
        }
      })
      setHistory(data);
    }).catch((err) => {
      console.error("Error fetching data:", err);
    });
  }

  // Tạo ma trận mới khi người dùng nhập số hàng và cột
  const handleGenerateMatrix = () => {
    if (rows <= 0 || columns <= 0) {
      setMessage("Số hàng và số cột phải lớn hơn 0.");
      setSnackbarSeverity("error");
      setOpenSnackbar(true);
      return;
    }
    const newMatrix = Array.from({ length: rows }, () => Array(columns).fill(0));
    setMatrix(newMatrix);
  };

  // Xử lý thay đổi giá trị trong ma trận
  const handleMatrixChange = (rowIndex: number, colIndex: number, value: string) => {
    const newMatrix = [...matrix];
    newMatrix[rowIndex][colIndex] = parseInt(value, 10) || 0;
    setMatrix(newMatrix);
  };

  // Gửi dữ liệu lên server và tính toán đường đi
  const handleSubmit = async () => {
    if (maxChest <= 0) {
      setMessage("Số rương tối đa phải lớn hơn 0.");
      setSnackbarSeverity("error");
      setOpenSnackbar(true);
      return;
    }
    const count = _.sumBy(matrix, subArray => _.countBy(subArray)[maxChest] || 0);

    if (count > 1) {
      setMessage("chỉ có 1 rương được có giá trị bằng giá trị của max chest");
      setSnackbarSeverity("error");
      setOpenSnackbar(true);
      return;
    }
    if (count == 0) {
      setMessage("phải bắt buộc có 1 rương có giá trị bằng giá trị của max chest");
      setSnackbarSeverity("error");
      setOpenSnackbar(true);
      return;
    }
    let countRow = matrix.length
    let countColumn = matrix[0].length
    if (countRow !== rows) {
      setMessage("số hàng của ma trận phải bằng số hàng nhập vào");
      setSnackbarSeverity("error");
      setOpenSnackbar(true);
      return;
    }
    if (countColumn !== columns) {
      setMessage("số cột của ma trận phải bằng số cột nhập vào");
      setSnackbarSeverity("error");
      setOpenSnackbar(true);
      return;
    }
    const input = {
      rows,
      columns,
      p: maxChest,
      matrix,
    };

    try {
      const response = await axios.post("http://localhost:17660/kho-bau/tim-kho-bau", input, {
        headers: { "Content-Type": "application/json" },
      });
      setShortestPath(response.data.data);
      setMessage("Tính toán thành công!");
      setSnackbarSeverity("success");
      setOpenSnackbar(true);
      getData()
      // Lưu thông tin vào lịch sử
      // const newHistory = [...history, { rows, columns, maxChest, matrix, shortestPath: response.data.data }];
      // setHistory(newHistory);
    } catch (error) {
      console.log("error:", error);
      setMessage("Lỗi khi tính toán.");
      setSnackbarSeverity("error");
      setOpenSnackbar(true);
    }
  };

  // Hàm để chuyển ma trận thành chuỗi hiển thị
  const formatMatrix = (matrix: any) => {

    return matrix?.map((row: any) => row.join(" ")).join("\n");
  };
  const runHistory = (data: any) => {
    setColumns(data.columns)
    setRows(data.rows)
    setMaxChest(data.p)
    setMatrix(data.matrix)
    setShortestPath(0)
  }
  const deleteData = (data: any) => {
    let confirm = window.confirm("Are you deleted data ?")
    if (confirm) {
      apiDeletedData(data.id).then((res: any) => {
        if (res.data.statusCode == 200) {
          setMessage(res.data.data)
          setSnackbarSeverity("error");
          setOpenSnackbar(true);
          getData()
        }

      })
    }

  }
  return (
    <div style={{ padding: "20px", height: window.innerHeight * 0.94, display: "flex", backgroundColor: '#efefef' }}>
      <div style={{ width: '38%', marginRight: '2%' }}>

        <Typography variant="h5" gutterBottom>
          Nhập Ma Trận
        </Typography>
        <Card>
          <div style={{ margin: '40px', height: window.innerHeight * 0.8 }}>
            <div style={{ height: '23%' }}>
              <Grid container spacing={2}>
                <Grid item>
                  <TextField
                    label="Số hàng"
                    value={rows}
                    onChange={(e) => setRows(parseInt(e.target.value, 10) || 0)}
                    fullWidth
                  />
                </Grid>
                <Grid item>
                  <TextField
                    label="Số cột"
                    value={columns}
                    onChange={(e) => setColumns(parseInt(e.target.value, 10) || 0)}
                    fullWidth
                  />
                </Grid>
                <Grid item>
                  <TextField
                    label="Số rương tối đa"
                    value={maxChest}
                    onChange={(e) => setMaxChest(parseInt(e.target.value, 10) || 0)}
                    fullWidth
                  />
                </Grid>

              </Grid>
              <Grid item style={{ marginTop: '15px' }}>
                <Button variant="contained" onClick={handleGenerateMatrix}>
                  Tạo Ma Trận
                </Button>
              </Grid>
            </div>

            {matrix.length > 0 && (
              <div style={{ height: '50%', overflow: 'auto', marginTop: '15%' }}>
                <Grid container spacing={1} style={{ marginTop: "20px", overflow: 'auto' }}>
                  {matrix.map((row, rowIndex) => (
                    <Grid container item key={rowIndex} spacing={1}>
                      {row.map((value, colIndex) => (
                        <Grid item key={`${rowIndex}-${colIndex}`}>
                          <TextField
                            type="number"
                            value={value}
                            onChange={(e) => handleMatrixChange(rowIndex, colIndex, e.target.value)}
                            style={{ width: "60px" }}
                            fullWidth
                          />
                        </Grid>
                      ))}
                    </Grid>
                  ))}
                </Grid>

              </div>

            )}
            {matrix.length > 0 && (
              <div style={{ marginTop: "20px", height: '6%', display: 'flex' }}>

                <Button variant="contained" color="primary" onClick={handleSubmit}>
                  Tìm Đường Đi
                </Button>

              </div>
            )}
            <div style={{ height: '5%', marginTop: '5px' }}>
              {shortestPath > 0 && <span >Tổng nhiên liệu ít nhất là: {shortestPath}</span>}
            </div>




            <Snackbar
              open={openSnackbar}
              autoHideDuration={6000}
              onClose={() => setOpenSnackbar(false)}
            >
              <Alert onClose={() => setOpenSnackbar(false)} severity={snackbarSeverity} sx={{ width: '100%' }}>
                {message}
              </Alert>
            </Snackbar>
          </div>

        </Card>

      </div>


      {/* Lịch sử nhập liệu */}
      <div style={{ width: '60%' }}>
        <Typography variant="h5" gutterBottom>
          Lịch sử Nhập Liệu
        </Typography>
        <TableContainer component={Paper} style={{ height: window.innerHeight * 0.9 }}>
          <Table>
            <TableHead>
              <TableRow>
                <TableCell>Số hàng</TableCell>
                <TableCell>Số cột</TableCell>
                <TableCell>Số rương tối đa</TableCell>
                <TableCell>Ma Trận</TableCell>
                <TableCell>Kho báu (Nhiên liệu)</TableCell>
                <TableCell>Action</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {history.map((entry, index) => (
                <TableRow key={index}>
                  <TableCell>{entry.rows}</TableCell>
                  <TableCell>{entry.columns}</TableCell>
                  <TableCell>{entry.p}</TableCell>

                  <TableCell>
                    <pre>{formatMatrix(entry.matrix)}</pre>
                  </TableCell>
                  <TableCell>{entry.totalFuel}</TableCell>
                  <TableCell>
                    <div style={{ display: 'flex' }}>
                      <div onClick={() => runHistory(entry)}><ArrowForwardIcon /></div>
                      <div onClick={() => deleteData(entry)}><DeleteIcon /></div>
                    </div>

                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </div>
    </div>
  );
};

export default TreasureForm;
