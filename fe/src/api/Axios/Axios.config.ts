import axios from 'axios';
import queryString from 'query-string';

const axiosClientIdd = axios.create({
  baseURL: "http://localhost:17660/kho-bau/"
    || '',
  headers: {
    'content-type': 'application/json',
  },
  paramsSerializer: (params) => queryString.stringify(params),
});





export default axiosClientIdd;
