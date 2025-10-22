import { UserNotification } from "../../../featuresLegacy/notification/models/UserNotification";
import { PagedData } from "../../../sharedLegacy/models/PagedDataT";

export interface GetNotificationsResponse {
    data: PagedData<UserNotification>
}