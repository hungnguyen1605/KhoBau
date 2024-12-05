import axios from "./Axios/Axios.config";
export const apiTimDuongDi = async(payload: any) => {
    return await axios.post('/tim-kho-bau', payload);
  };
export const apiGetData = async() => {
    return await axios.get('/get-data');
  };
export const apiDeletedData = async(id:any) => {
    return await axios.delete(`/delete-data/${id}`);
  };
