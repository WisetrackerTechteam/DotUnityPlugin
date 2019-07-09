//
//  XConstants.h
//  XTestApp
//
//  Created by Woncheol Heo on 2019. 4. 3..
//  Copyright © 2019년 wisetracker. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

@interface XConstants : NSObject

typedef enum {
    NEW_SESSION = 0,
    USER_IDENTIFY,
    GROUP_IDENTIFY,
    EVENT_DATA,
    CONVERSION_DATA,
    REVENUE_DATA,
    DEEPLINK
} DataType;

extern const int kDOXEventUploadPeriodSeconds;

//extern NSString *const IDENTIFY_EVENT;
//extern NSString *const GROUP_IDENTIFY_EVENT;
extern NSString *const DOX_OP_ADD;
extern NSString *const DOX_OP_APPEND;
extern NSString *const DOX_OP_CLEAR_ALL;
extern NSString *const DOX_OP_PREPEND;
extern NSString *const DOX_OP_SET;
extern NSString *const DOX_OP_SET_ONCE;
extern NSString *const DOX_OP_UNSET;
extern NSString *const DOX_VERSION;

extern NSString *const SESSION;
extern NSString *const USER_PROPERTIES;
extern NSString *const GROUP_PROPERTIES;
extern NSString *const GROUPS;
extern NSString *const EVENT;
extern NSString *const CONVERSION;
extern NSString *const REVENUE;
@end

NS_ASSUME_NONNULL_END
