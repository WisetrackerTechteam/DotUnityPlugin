//
//  NetworkManager.h
//  DOX
//
//  Created by Woncheol Heo on 2019. 4. 9..
//  Copyright © 2019년 wisetracker. All rights reserved.
//

#import <Foundation/Foundation.h>

NS_ASSUME_NONNULL_BEGIN

typedef void(^CompletionBlock)(BOOL isSuccess, NSData *data, id respons);

@interface XNetworkManager : NSObject

- (void)sendDocument:(NSString *)fianlJsonListString completion:(CompletionBlock)completion;

@end

NS_ASSUME_NONNULL_END
