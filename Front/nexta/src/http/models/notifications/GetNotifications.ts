import { UserNotification } from "../../../features/notification/models/UserNotification";
import { PagedData } from "../../../shared/models/PagedDataT";

export interface GetNotificationsResponse {
    data: PagedData<UserNotification>
}